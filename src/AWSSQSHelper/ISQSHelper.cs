﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.SQS.Model;

namespace AWSSQSHelper
{
    /// <summary>
    /// Functions to interact with the AWS SQS service
    /// </summary>
    public interface ISQSHelper : IDisposable
    {
        /// <summary>
        /// Create queue
        /// </summary>
        /// <param name="queueName">URL of queue</param>
        /// <param name="attributes">Attributes to give the queue</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<CreateQueueResponse> CreateQueueAsync(string queueName,
            Dictionary<string, string> attributes = null);

        /// <summary>
        /// Delete message from a queue
        /// </summary>
        /// <param name="queueUrl">URL of queue</param>
        /// <param name="receiptHandle">Receipt handle of message to delete</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<DeleteMessageResponse> DeleteMessageAsync(string queueUrl,
            string receiptHandle);

        /// <summary>
        /// Delete messages from from a queue
        /// </summary>
        /// <param name="queueUrl">URL of queue</param>
        /// <param name="receiptHandles">Receipt handles of messages to delete</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Task<DeleteMessageBatchResponse> DeleteMessagesAsync(string queueUrl,
            IEnumerable<string> receiptHandles);

        /// <summary>
        /// Delete queue
        /// </summary>
        /// <param name="queueUrl">URL of queue</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<DeleteQueueResponse> DeleteQueueAsync(string queueUrl);

        /// <summary>
        /// Get number of messages on a queue
        /// </summary>
        /// <param name="queueUrl">URL of queue</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<int> GetNumberOfMessagesOnQueueAsync(string queueUrl);

        /// <summary>
        /// Get queue attributes
        /// </summary>
        /// <param name="queueUrl">URL of queue</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<GetQueueAttributesResponse> GetQueueAttributesAsync(string queueUrl);

        /// <summary>
        /// Purge all messages on a queue
        /// </summary>
        /// <param name="queueUrl">URL of queue</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<PurgeQueueResponse> PurgeQueueAsync(string queueUrl);

        /// <summary>
        /// Receieve all the messages on a queue
        /// </summary>
        /// <param name="queueUrl">URL of queue</param>
        /// <param name="visibilityTimeout">Time in seconds that a message is hidden from the queue. Default is 10</param>
        /// <param name="attributeNames">List of attribute names that need to be returned for each message. Default is 'ALL'</param>
        /// <param name="messageAttributeNames">List of message attributes to be returned for each message</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<IEnumerable<Message>> ReceiveAllMessagesAsync(string queueUrl,
            int visibilityTimeout = 10,
            IEnumerable<string> attributeNames = null,
            IEnumerable<string> messageAttributeNames = null);

        /// <summary>
        /// Receive messages from a queue
        /// </summary>
        /// <param name="queueUrl">URL of queue</param>
        /// <param name="maxNumberOfMessages">Maximum number of messages to receive. Default and maximum is 10</param>
        /// <param name="visibilityTimeout">Time in seconds that a message is hidden from the queue. Default is 10</param>
        /// <param name="attributeNames">List of attribute names that need to be returned for each message. Default is 'ALL'</param>
        /// <param name="messageAttributeNames">List of message attributes to be returned for each message</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<ReceiveMessageResponse> ReceiveMessagesAsync(string queueUrl,
            int maxNumberOfMessages = 10,
            int visibilityTimeout = 10,
            IEnumerable<string> attributeNames = null,
            IEnumerable<string> messageAttributeNames = null);

        /// <summary>
        /// Send message to a queue
        /// </summary>
        /// <param name="queueUrl">URL of queue</param>
        /// <param name="messageBody">Body of message</param>
        /// <param name="delaySeconds">How long to delay sending message. Default is 0</param>
        /// <param name="messageAttributes">Attributes to send with the message</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<SendMessageResponse> SendMessageAsync(string queueUrl,
            string messageBody,
            int delaySeconds = 0,
            Dictionary<string, MessageAttributeValue> messageAttributes = null);

        /// <summary>
        /// Send messages to a queue
        /// </summary>
        /// <param name="queueUrl">URL of queue</param>
        /// <param name="messages">Messages</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Task<SendMessageBatchResponse> SendMessagesAsync(string queueUrl,
            IEnumerable<SendMessageBatchRequestEntry> messages);
    }
}