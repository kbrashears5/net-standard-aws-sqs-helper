using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AWSSQSHelper
{
    /// <summary>
    /// Implementation of <see cref="ISQSHelper"/>
    /// </summary>
    public class SQSHelper : ISQSHelper
    {
        /// <summary>
        /// Logger
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// Client to interact with AWS SQS
        /// </summary>
        private AmazonSQSClient Repository { get; }

        /// <summary>
        /// Cancellation Token
        /// </summary>
        private CancellationTokenSource CancellationToken { get; }

        /// <summary>
        /// Creates a new instance of <see cref="SQSHelper"/>
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="repository"><see cref="AmazonSQSClient"/> - If none is provided, will create new instance in account that lambda is running in</param>
        /// <param name="options"><see cref="AmazonSQSConfig"/>- Only used if the repository is not supplied. Defaults to <see cref="RegionEndpoint.USEast1"/> Region</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SQSHelper(ILogger logger,
            AmazonSQSClient repository = null,
            AmazonSQSConfig options = null)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (options == null) options = new AmazonSQSConfig { RegionEndpoint = RegionEndpoint.USEast1 };

            this.Repository = repository ?? new AmazonSQSClient(config: options);

            this.CancellationToken = new CancellationTokenSource();
        }

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
                    try
                    {
                        this.CancellationToken?.Cancel();
                    }
                    catch (ObjectDisposedException)
                    {
                    }

                    this.CancellationToken?.Dispose();

                    this.Repository?.Dispose();

                    this.Logger?.Dispose();
                }

                this.Disposed = true;
            }
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~SQSHelper() => this.Dispose(disposing: false);

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

        public async Task<CreateQueueResponse> CreateQueueAsync(string queueName,
            Dictionary<string, string> attributes = null,
            CancellationToken cancellationToken = default)
        {
            this.Logger.LogDebug($"[{nameof(this.CreateQueueAsync)}]");

            this.Logger.LogTrace(JsonConvert.SerializeObject(new { queueName, attributes }));

            if (string.IsNullOrWhiteSpace(queueName)) throw new ArgumentNullException(nameof(queueName));

            var request = new Amazon.SQS.Model.CreateQueueRequest
            {
                Attributes = attributes,
                QueueName = queueName,
            };

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: request));

            var response = await this.Repository.CreateQueueAsync(request: request,
                cancellationToken: cancellationToken == default ? this.CancellationToken.Token : cancellationToken);

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: response));

            return response;
        }

        public async Task<DeleteMessageResponse> DeleteMessageAsync(string queueUrl,
            string receiptHandle,
            CancellationToken cancellationToken = default)
        {
            this.Logger.LogDebug($"[{nameof(this.DeleteMessageAsync)}]");

            this.Logger.LogTrace(JsonConvert.SerializeObject(new { queueUrl, receiptHandle }));

            if (string.IsNullOrWhiteSpace(queueUrl)) throw new ArgumentNullException(nameof(queueUrl));
            if (string.IsNullOrWhiteSpace(receiptHandle)) throw new ArgumentNullException(nameof(receiptHandle));

            var request = new Amazon.SQS.Model.DeleteMessageRequest
            {
                QueueUrl = queueUrl,
                ReceiptHandle = receiptHandle,
            };

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: request));

            var response = await this.Repository.DeleteMessageAsync(request: request,
                cancellationToken: cancellationToken == default ? this.CancellationToken.Token : cancellationToken);

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: response));

            return response;
        }

        public async Task<DeleteMessageBatchResponse> DeleteMessagesAsync(string queueUrl,
            IEnumerable<string> receiptHandles,
            CancellationToken cancellationToken = default)
        {
            this.Logger.LogDebug($"[{nameof(this.DeleteMessagesAsync)}]");

            this.Logger.LogTrace(JsonConvert.SerializeObject(new { queueUrl, receiptHandles }));

            if (string.IsNullOrWhiteSpace(queueUrl)) throw new ArgumentNullException(nameof(queueUrl));
            if (receiptHandles == null) throw new ArgumentNullException(nameof(receiptHandles));
            if (receiptHandles.Count() == 0) throw new ArgumentException(nameof(receiptHandles));

            var request = new Amazon.SQS.Model.DeleteMessageBatchRequest
            {
                QueueUrl = queueUrl,
                Entries = receiptHandles.Select(r => new DeleteMessageBatchRequestEntry(id: Guid.NewGuid().ToString(), receiptHandle: r)).ToList(),
            };

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: request));

            var response = await this.Repository.DeleteMessageBatchAsync(request: request,
                cancellationToken: cancellationToken == default ? this.CancellationToken.Token : cancellationToken);

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: response));

            return response;
        }

        public async Task<DeleteQueueResponse> DeleteQueueAsync(string queueUrl,
            CancellationToken cancellationToken = default)
        {
            this.Logger.LogDebug($"[{nameof(this.DeleteQueueAsync)}]");

            this.Logger.LogTrace(JsonConvert.SerializeObject(new { queueUrl }));

            if (string.IsNullOrWhiteSpace(queueUrl)) throw new ArgumentNullException(nameof(queueUrl));

            var request = new Amazon.SQS.Model.DeleteQueueRequest
            {
                QueueUrl = queueUrl,
            };

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: request));

            var response = await this.Repository.DeleteQueueAsync(request: request,
                cancellationToken: cancellationToken == default ? this.CancellationToken.Token : cancellationToken);

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: response));

            return response;
        }

        public async Task<int> GetNumberOfMessagesOnQueueAsync(string queueUrl,
            CancellationToken cancellationToken = default)
        {
            this.Logger.LogDebug($"[{nameof(this.GetNumberOfMessagesOnQueueAsync)}]");

            this.Logger.LogTrace(JsonConvert.SerializeObject(new { queueUrl }));

            if (string.IsNullOrWhiteSpace(queueUrl)) throw new ArgumentNullException(nameof(queueUrl));

            var request = new Amazon.SQS.Model.GetQueueAttributesRequest
            {
                QueueUrl = queueUrl,
                AttributeNames = new List<string>() { Text.ApproximateNumberOfMessages },
            };

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: request));

            var response = await this.Repository.GetQueueAttributesAsync(request: request,
                cancellationToken: cancellationToken == default ? this.CancellationToken.Token : cancellationToken);

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: response));

            return response.ApproximateNumberOfMessages;
        }

        public async Task<GetQueueAttributesResponse> GetQueueAttributesAsync(string queueUrl,
            CancellationToken cancellationToken = default)
        {
            this.Logger.LogDebug($"[{nameof(this.GetQueueAttributesAsync)}]");

            this.Logger.LogTrace(JsonConvert.SerializeObject(new { queueUrl }));

            if (string.IsNullOrWhiteSpace(queueUrl)) throw new ArgumentNullException(nameof(queueUrl));

            var request = new Amazon.SQS.Model.GetQueueAttributesRequest
            {
                QueueUrl = queueUrl,
                AttributeNames = new List<string>() { Text.All },
            };

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: request));

            var response = await this.Repository.GetQueueAttributesAsync(request: request,
                cancellationToken: cancellationToken == default ? this.CancellationToken.Token : cancellationToken);

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: response));

            return response;
        }

        public async Task<PurgeQueueResponse> PurgeQueueAsync(string queueUrl,
            CancellationToken cancellationToken = default)
        {
            this.Logger.LogDebug($"[{nameof(this.PurgeQueueAsync)}]");

            this.Logger.LogTrace(JsonConvert.SerializeObject(new { queueUrl }));

            if (string.IsNullOrWhiteSpace(queueUrl)) throw new ArgumentNullException(nameof(queueUrl));

            var request = new Amazon.SQS.Model.PurgeQueueRequest
            {
                QueueUrl = queueUrl,
            };

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: request));

            try
            {
                var response = await this.Repository.PurgeQueueAsync(request: request,
                cancellationToken: cancellationToken == default ? this.CancellationToken.Token : cancellationToken);

                this.Logger.LogTrace(JsonConvert.SerializeObject(value: response));

                return response;
            }
            catch (Amazon.SQS.Model.PurgeQueueInProgressException ex)
            {
                this.Logger.LogError(ex.Message);

                return null;
            }
        }

        public async Task<IEnumerable<Message>> ReceiveAllMessagesAsync(string queueUrl,
            int visibilityTimeout = 10,
            IEnumerable<string> attributeNames = null,
            IEnumerable<string> messageAttributeNames = null,
            CancellationToken cancellationToken = default)
        {
            this.Logger.LogDebug($"[{nameof(this.ReceiveAllMessagesAsync)}]");

            this.Logger.LogTrace(JsonConvert.SerializeObject(new { queueUrl, visibilityTimeout, attributeNames, messageAttributeNames }));

            if (string.IsNullOrWhiteSpace(queueUrl)) throw new ArgumentNullException(nameof(queueUrl));

            var messages = new List<Message>();

            var localCancellationToken = cancellationToken == default ? this.CancellationToken.Token : cancellationToken;

            var count = 0;

            do
            {
                var currentMessages = await this.ReceiveMessagesAsync(queueUrl: queueUrl,
                    visibilityTimeout: visibilityTimeout,
                    attributeNames: attributeNames,
                    messageAttributeNames: messageAttributeNames);

                count = currentMessages.Messages.Count;

                messages.AddRange(currentMessages.Messages);
            } while (count > 0 && !localCancellationToken.IsCancellationRequested);

            return messages;
        }

        public async Task<ReceiveMessageResponse> ReceiveMessagesAsync(string queueUrl,
            int maxNumberOfMessages = 10,
            int visibilityTimeout = 10,
            IEnumerable<string> attributeNames = null,
            IEnumerable<string> messageAttributeNames = null,
            CancellationToken cancellationToken = default)
        {
            this.Logger.LogDebug($"[{nameof(this.ReceiveMessagesAsync)}]");

            this.Logger.LogTrace(JsonConvert.SerializeObject(new { queueUrl, maxNumberOfMessages, visibilityTimeout, attributeNames, messageAttributeNames }));

            if (string.IsNullOrWhiteSpace(queueUrl)) throw new ArgumentNullException(nameof(queueUrl));

            var request = new Amazon.SQS.Model.ReceiveMessageRequest
            {
                AttributeNames = attributeNames == null ? new List<string>() { Text.All } : attributeNames.ToList(),
                QueueUrl = queueUrl,
                MaxNumberOfMessages = maxNumberOfMessages,
                MessageAttributeNames = messageAttributeNames.ToList(),
                VisibilityTimeout = visibilityTimeout,
            };

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: request));

            var response = await this.Repository.ReceiveMessageAsync(request: request,
                cancellationToken: cancellationToken == default ? this.CancellationToken.Token : cancellationToken);

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: response));

            return response;
        }

        public async Task<SendMessageResponse> SendMessageAsync(string queueUrl,
            string messageBody,
            int delaySeconds = 0,
            Dictionary<string, MessageAttributeValue> messageAttributes = null,
            CancellationToken cancellationToken = default)
        {
            this.Logger.LogDebug($"[{nameof(this.SendMessageAsync)}]");

            this.Logger.LogTrace(JsonConvert.SerializeObject(new { queueUrl, messageBody, delaySeconds, messageAttributes }));

            if (string.IsNullOrWhiteSpace(queueUrl)) throw new ArgumentNullException(nameof(queueUrl));
            if (string.IsNullOrWhiteSpace(messageBody)) throw new ArgumentNullException(nameof(messageBody));

            var request = new Amazon.SQS.Model.SendMessageRequest
            {
                DelaySeconds = delaySeconds,
                MessageAttributes = messageAttributes,
                MessageBody = messageBody,
                QueueUrl = queueUrl,
            };

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: request));

            var response = await this.Repository.SendMessageAsync(request: request,
                cancellationToken: cancellationToken == default ? this.CancellationToken.Token : cancellationToken);

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: response));

            return response;
        }

        public async Task<SendMessageBatchResponse> SendMessagesAsync(string queueUrl,
            IEnumerable<SendMessageBatchRequestEntry> messages,
            CancellationToken cancellationToken = default)
        {
            this.Logger.LogDebug($"[{nameof(this.SendMessagesAsync)}]");

            this.Logger.LogTrace(JsonConvert.SerializeObject(new { queueUrl, messages }));

            if (string.IsNullOrWhiteSpace(queueUrl)) throw new ArgumentNullException(nameof(queueUrl));
            if (messages == null) throw new ArgumentNullException(nameof(messages));
            if (messages.Count() == 0) throw new ArgumentException(nameof(messages));

            var request = new Amazon.SQS.Model.SendMessageBatchRequest
            {
                Entries = messages.ToList(),
                QueueUrl = queueUrl,
            };

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: request));

            var response = await this.Repository.SendMessageBatchAsync(request: request,
                cancellationToken: cancellationToken == default ? this.CancellationToken.Token : cancellationToken);

            this.Logger.LogTrace(JsonConvert.SerializeObject(value: response));

            return response;
        }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}