﻿using System;
using System.Data.Common;
using System.Threading.Tasks;

using LinqToDB.Tools;

namespace LinqToDB.Data
{
	using Interceptors;

	/// <summary>
	/// Disposable wrapper over <see cref="DbDataReader"/> instance, which properly disposes associated objects.
	/// </summary>
	public class DataReaderWrapper : IDisposable
#if !NETFRAMEWORK
		, IAsyncDisposable
#endif
	{
		private          bool            _disposed;
		private readonly DataConnection? _dataConnection;
		private readonly IDisposable?    _concurrencyToken;

		/// <summary>
		/// Creates wrapper instance for specified data reader.
		/// </summary>
		/// <param name="dataReader">Wrapped data reader instance.</param>
		public DataReaderWrapper(DbDataReader dataReader)
		{
			DataReader = dataReader;
		}

		internal DataReaderWrapper(DataConnection dataConnection, DbDataReader dataReader, DbCommand? command, IDisposable concurrencyToken)
		{
			_dataConnection   = dataConnection;
			DataReader        = dataReader;
			Command           = command;
			_concurrencyToken = concurrencyToken;
		}

		public   DbDataReader? DataReader { get; private set; }
		internal DbCommand?    Command    { get; }

		internal Action<DbCommand>? OnBeforeCommandDispose { get; set; }

		public void Dispose()
		{
			if (_disposed)
				return;

			_disposed = true;

			if (DataReader != null)
			{
				if (_dataConnection is IInterceptable<ICommandInterceptor> { Interceptor: {} interceptor })
					using (ActivityService.Start(ActivityID.CommandInterceptorBeforeReaderDispose))
						interceptor.BeforeReaderDispose(new (_dataConnection), Command, DataReader);

				DataReader.Dispose();
				DataReader = null;
			}

			if (Command != null)
			{
				OnBeforeCommandDispose?.Invoke(Command);
				OnBeforeCommandDispose = null;

				if (_dataConnection != null)
					_dataConnection.DataProvider.DisposeCommand(Command);
				else
					Command.Dispose();
			}

			if (_concurrencyToken != null)
				_concurrencyToken.Dispose();
		}

#if NETSTANDARD2_1PLUS
		public async ValueTask DisposeAsync()
		{
			if (_disposed)
				return;

			_disposed = true;

			if (DataReader != null)
			{
				if (_dataConnection is IInterceptable<ICommandInterceptor> { Interceptor: {} interceptor })
					await using (ActivityService.StartAndConfigureAwait(ActivityID.CommandInterceptorBeforeReaderDisposeAsync))
						await interceptor.BeforeReaderDisposeAsync(new(_dataConnection), Command, DataReader)
							.ConfigureAwait(Common.Configuration.ContinueOnCapturedContext);

				await DataReader.DisposeAsync().ConfigureAwait(Common.Configuration.ContinueOnCapturedContext);
				DataReader = null;
			}

			if (Command != null)
			{
				OnBeforeCommandDispose?.Invoke(Command);

				if (_dataConnection != null)
					await _dataConnection.DataProvider.DisposeCommandAsync(Command).ConfigureAwait(Common.Configuration.ContinueOnCapturedContext);
				else
					await Command.DisposeAsync().ConfigureAwait(Common.Configuration.ContinueOnCapturedContext);
			}

			if (_concurrencyToken != null)
				_concurrencyToken.Dispose();
		}
#elif !NETFRAMEWORK
		public ValueTask DisposeAsync()
		{
			Dispose();
			return new ValueTask(Task.CompletedTask);
		}
#endif
	}
}
