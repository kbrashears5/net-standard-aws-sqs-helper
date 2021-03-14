using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AWSSQSHelper
{
    /// <summary>
    /// Mock implementation of <see cref="ISQSHelper"/>
    /// </summary>
    public class SQSHelper_Mock : ISQSHelper
    {
        #region IDisposable

        /// <summary>
        /// Disposed
        /// </summary>
        private bool Disposed { get; set; } = false;

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                }

                this.Disposed = true;
            }
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~SQSHelper_Mock() => this.Dispose(disposing: false);

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            this.Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }

        #endregion IDisposable

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        public Task<CreateQueueResponse> CreateQueueAsync(string queueName,
            Dictionary<string, string> attributes,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new CreateQueueResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
            });
        }

        public Task<DeleteMessageResponse> DeleteMessageAsync(string queueUrl,
            string receiptHandle,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new DeleteMessageResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
            });
        }

        public Task<DeleteMessageBatchResponse> DeleteMessagesAsync(string queueUrl,
            IEnumerable<string> receiptHandles,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new DeleteMessageBatchResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
            });
        }

        public Task<DeleteQueueResponse> DeleteQueueAsync(string queueUrl,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new DeleteQueueResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
            });
        }

        public Task<int> GetNumberOfMessagesOnQueueAsync(string queueUrl,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(7);
        }

        public Task<GetQueueAttributesResponse> GetQueueAttributesAsync(string queueUrl,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new GetQueueAttributesResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
            });
        }

        public Task<PurgeQueueResponse> PurgeQueueAsync(string queueUrl,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new PurgeQueueResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
            });
        }

        public Task<IEnumerable<Message>> ReceiveAllMessagesAsync(string queueUrl,
            int visibilityTimeout = 10,
            IEnumerable<string> attributeNames = null,
            IEnumerable<string> messageAttributeNames = null,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new List<Message>() { new Message() }.AsEnumerable());
        }

        public Task<ReceiveMessageResponse> ReceiveMessagesAsync(string queueUrl,
            int maxNumberOfMessages = 10,
            int visibilityTimeout = 10,
            IEnumerable<string> attributeNames = null,
            IEnumerable<string> messageAttributeNames = null,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new ReceiveMessageResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
            });
        }

        public Task<SendMessageResponse> SendMessageAsync(string queueUrl,
            string messageBody,
            int delaySeconds = 0,
            Dictionary<string, MessageAttributeValue> messageAttributes = null,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new SendMessageResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
            });
        }

        public Task<SendMessageBatchResponse> SendMessagesAsync(string queueUrl,
            IEnumerable<SendMessageBatchRequestEntry> messages,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new SendMessageBatchResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
            });
        }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}