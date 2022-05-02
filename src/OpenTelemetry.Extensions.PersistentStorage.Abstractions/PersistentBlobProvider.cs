﻿// <copyright file="PersistentBlobProvider.cs" company="OpenTelemetry Authors">
// Copyright The OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenTelemetry.Extensions.PersistentStorage.Abstractions
{
    /// <summary>
    /// Represents persistent blob provider.
    /// </summary>
    public abstract class PersistentBlobProvider
    {
        /// <summary>
        /// Attempts to create a new blob with the provided data and lease it.
        /// </summary>
        /// <param name="buffer">
        /// The content to be written.
        /// </param>
        /// <param name="leasePeriodMilliseconds">
        /// The number of milliseconds to lease after the blob is created.
        /// </param>
        /// <param name="blob">
        /// Blob if it is created.
        /// </param>
        /// <returns>
        /// True if the blob was created or else false.
        /// </returns>
        public bool TryCreateBlob(byte[] buffer, int leasePeriodMilliseconds, out PersistentBlob blob)
        {
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                return this.OnTryCreateBlob(buffer, leasePeriodMilliseconds, out blob);
            }
            catch (Exception)
            {
                // TODO: log exception.
                blob = null;
                return false;
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        /// <summary>
        /// Attempts to create a new blob with the provided data.
        /// </summary>
        /// <param name="buffer">
        /// The content to be written.
        /// </param>
        /// <param name="blob">
        /// Blob if it is created.
        /// </param>
        /// <returns>
        /// True if the blob was created or else false.
        /// </returns>
        public bool TryCreateBlob(byte[] buffer, out PersistentBlob blob)
        {
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                return this.OnTryCreateBlob(buffer, out blob);
            }
            catch (Exception)
            {
                // TODO: log exception;
                blob = null;
                return false;
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        /// <summary>
        /// Attempts to get a single blob from storage.
        /// </summary>
        /// <param name="blob">
        /// Blob object if found.
        /// </param>
        /// <returns>
        /// True if blob is present or else false.
        /// </returns>
        public bool TryGetBlob(out PersistentBlob blob)
        {
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                return this.OnTryGetBlob(out blob);
            }
            catch (Exception)
            {
                // TODO: log exception.
                blob = null;
                return false;
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        /// <summary>
        /// Reads a sequence of blobs from storage.
        /// </summary>
        /// <returns>
        /// List of blobs if present in storage or else empty collection.
        /// </returns>
        public IEnumerable<PersistentBlob> GetBlobs()
        {
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                return this.OnGetBlobs() ?? Enumerable.Empty<PersistentBlob>();
            }
            catch (Exception)
            {
                // TODO: log exception
                return Enumerable.Empty<PersistentBlob>();
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        protected abstract IEnumerable<PersistentBlob> OnGetBlobs();

        protected abstract bool OnTryCreateBlob(byte[] buffer, int leasePeriodMilliseconds, out PersistentBlob blob);

        protected abstract bool OnTryCreateBlob(byte[] buffer, out PersistentBlob blob);

        protected abstract bool OnTryGetBlob(out PersistentBlob blob);
    }
}
