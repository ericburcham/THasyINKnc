using System;
using System.IO;

namespace ShuttingDown
{
    public class DirectoryWriter
    {
        public DirectoryWriter(string path)
        {
            _path = path;
        }

        /// <summary>
        ///     Returns whether the worker thread has been asked to stop.
        ///     This continues to return true even after the thread has stopped.
        /// </summary>
        public bool Stopping
        {
            get
            {
                lock (_stopLock)
                {
                    return _stopping;
                }
            }
        }

        /// <summary>
        ///     Returns whether the worker thread has stopped.
        /// </summary>
        public bool Stopped
        {
            get
            {
                lock (_stopLock)
                {
                    return _stopped;
                }
            }
        }

        /// <summary>
        ///     Tells the worker thread to stop, typically after completing its
        ///     current work item. (The thread is *not* guaranteed to have stopped
        ///     by the time this method returns.)
        /// </summary>
        public void Stop()
        {
            lock (_stopLock)
            {
                _stopping = true;
            }
        }

        /// <summary>
        ///     Called by the worker thread to indicate when it has stopped.
        /// </summary>
        private void SetStopped()
        {
            lock (_stopLock)
            {
                _stopped = true;
            }
        }

        /// <summary>
        ///     Main work loop of the class.
        /// </summary>
        public void Write()
        {
            try
            {
                Write(_path);
            }
            finally
            {
                SetStopped();
            }
        }

        private void Write(string path)
        {
            try
            {
                foreach (var directory in Directory.GetDirectories(path))
                {
                    if (Stopping)
                    {
                        return;
                    }
                    foreach (var file in Directory.GetFiles(directory))
                    {
                        Console.WriteLine(file);
                    }
                    Write(directory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private readonly string _path;

        /// <summary>
        ///     Lock covering stopping and stopped
        /// </summary>
        private readonly object _stopLock = new object();

        /// <summary>
        ///     Whether or not the worker thread has stopped
        /// </summary>
        private bool _stopped;

        /// <summary>
        ///     Whether or not the worker thread has been asked to stop
        /// </summary>
        private bool _stopping;
    }
}