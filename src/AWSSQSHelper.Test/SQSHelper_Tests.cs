using Amazon.SQS.Model;
using Logger;
using NetStandardTestHelper.Xunit;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace AWSSQSHelper.Test
{
    /// <summary>
    /// Test the <see cref="SQSHelper"/> class
    /// </summary>
    public class SQSHelper_Tests
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Test that the constructor throws on null <see cref="ILogger"/>
        /// </summary>
        [Fact]
        public void Constructor_Null_Logger()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new SQSHelper(logger: null));

            TestHelper.AssertArgumentNullException(ex,
                "logger");
        }

        /// <summary>
        /// Test that the constructor is created successfully
        /// </summary>
        [Fact]
        public void Constructor()
        {
            var helper = TestValues.SQSHelper;

            Assert.NotNull(helper);
        }

        #endregion CONSTRUCTOR

        #region CreateQueueAsync

        /// <summary>
        /// Test that function throws on null queue name
        /// </summary>
        [Fact]
        public async void CreateQueueAsync_Null_QueueName()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.CreateQueueAsync(queueName: NetStandardTestHelper.TestValues.StringEmpty));

            TestHelper.AssertArgumentNullException(ex,
                "queueName");
        }

        /// <summary>
        /// Test that function creates queue
        /// </summary>
        [Fact]
        public async void CreateQueueAsync()
        {
            var response = await TestValues.SQSHelper_Mock.CreateQueueAsync(queueName: TestValues.QueueName);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK,
                response.HttpStatusCode);
        }

        #endregion CreateQueueAsync

        #region DeleteMessageAsync

        /// <summary>
        /// Test that function throws on null queue url
        /// </summary>
        [Fact]
        public async void DeleteMessageAsync_Null_QueueUrl()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.DeleteMessageAsync(queueUrl: NetStandardTestHelper.TestValues.StringEmpty,
                receiptHandle: TestValues.ReceiptHandle));

            TestHelper.AssertArgumentNullException(ex,
                "queueUrl");
        }

        /// <summary>
        /// Test that function throws on null receipt handle
        /// </summary>
        [Fact]
        public async void DeleteMessageAsync_Null_ReceiptHandle()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.DeleteMessageAsync(queueUrl: TestValues.QueueUrl,
                receiptHandle: NetStandardTestHelper.TestValues.StringEmpty));

            TestHelper.AssertArgumentNullException(ex,
                "receiptHandle");
        }

        /// <summary>
        /// Test that function deletes message
        /// </summary>
        [Fact]
        public async void DeleteMessageAsync()
        {
            var response = await TestValues.SQSHelper_Mock.DeleteMessageAsync(queueUrl: TestValues.QueueName,
                receiptHandle: TestValues.ReceiptHandle);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK,
                response.HttpStatusCode);
        }

        #endregion DeleteMessageAsync

        #region DeleteMessagesAsync

        /// <summary>
        /// Test that function throws on null queue url
        /// </summary>
        [Fact]
        public async void DeleteMessagesAsync_Null_QueueUrl()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.DeleteMessagesAsync(queueUrl: NetStandardTestHelper.TestValues.StringEmpty,
                receiptHandles: new List<string>() { TestValues.ReceiptHandle }));

            TestHelper.AssertArgumentNullException(ex,
                "queueUrl");
        }

        /// <summary>
        /// Test that function throws on null receipt handle
        /// </summary>
        [Fact]
        public async void DeleteMessagesAsync_Null_ReceiptHandles()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.DeleteMessagesAsync(queueUrl: TestValues.QueueUrl,
                receiptHandles: null));

            TestHelper.AssertArgumentNullException(ex,
                "receiptHandles");
        }

        /// <summary>
        /// Test that function deletes messages
        /// </summary>
        [Fact]
        public async void DeleteMessagesAsync()
        {
            var response = await TestValues.SQSHelper_Mock.DeleteMessagesAsync(queueUrl: TestValues.QueueName,
                receiptHandles: new List<string>() { TestValues.ReceiptHandle });

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK,
                response.HttpStatusCode);
        }

        #endregion DeleteMessagesAsync

        #region DeleteQueueAsync

        /// <summary>
        /// Test that function throws on null queue url
        /// </summary>
        [Fact]
        public async void DeleteQueueAsync_Null_QueueUrl()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.DeleteQueueAsync(queueUrl: NetStandardTestHelper.TestValues.StringEmpty));

            TestHelper.AssertArgumentNullException(ex,
                "queueUrl");
        }

        /// <summary>
        /// Test that function deletes queue
        /// </summary>
        [Fact]
        public async void DeleteQueueAsync()
        {
            var response = await TestValues.SQSHelper_Mock.DeleteQueueAsync(queueUrl: TestValues.QueueName);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK,
                response.HttpStatusCode);
        }

        #endregion DeleteQueueAsync

        #region GetNumberOfMessagesOnQueueAsync

        /// <summary>
        /// Test that function throws on null queue url
        /// </summary>
        [Fact]
        public async void GetNumberOfMessagesOnQueueAsync_Null_QueueUrl()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.GetNumberOfMessagesOnQueueAsync(queueUrl: NetStandardTestHelper.TestValues.StringEmpty));

            TestHelper.AssertArgumentNullException(ex,
                "queueUrl");
        }

        /// <summary>
        /// Test that function gets number of messages
        /// </summary>
        [Fact]
        public async void GetNumberOfMessagesOnQueueAsync()
        {
            var response = await TestValues.SQSHelper_Mock.GetNumberOfMessagesOnQueueAsync(queueUrl: TestValues.QueueName);

            Assert.Equal(7,
                response);
        }

        #endregion GetNumberOfMessagesOnQueueAsync

        #region GetQueueAttributesAsync

        /// <summary>
        /// Test that function throws on null queue url
        /// </summary>
        [Fact]
        public async void GetQueueAttributesAsync_Null_QueueUrl()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.GetQueueAttributesAsync(queueUrl: NetStandardTestHelper.TestValues.StringEmpty));

            TestHelper.AssertArgumentNullException(ex,
                "queueUrl");
        }

        /// <summary>
        /// Test that function gets queue attributes
        /// </summary>
        [Fact]
        public async void GetQueueAttributesAsync()
        {
            var response = await TestValues.SQSHelper_Mock.GetQueueAttributesAsync(queueUrl: TestValues.QueueName);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK,
                response.HttpStatusCode);
        }

        #endregion GetQueueAttributesAsync

        #region PurgeQueueAsync

        /// <summary>
        /// Test that function throws on null queue url
        /// </summary>
        [Fact]
        public async void PurgeQueueAsync_Null_QueueUrl()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.PurgeQueueAsync(queueUrl: NetStandardTestHelper.TestValues.StringEmpty));

            TestHelper.AssertArgumentNullException(ex,
                "queueUrl");
        }

        /// <summary>
        /// Test that function purges queue
        /// </summary>
        [Fact]
        public async void PurgeQueueAsync()
        {
            var response = await TestValues.SQSHelper_Mock.PurgeQueueAsync(queueUrl: TestValues.QueueName);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK,
                response.HttpStatusCode);
        }

        #endregion PurgeQueueAsync

        #region ReceiveAllMessagesAsync

        /// <summary>
        /// Test that function throws on null queue url
        /// </summary>
        [Fact]
        public async void ReceiveAllMessagesAsync_Null_QueueUrl()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.ReceiveAllMessagesAsync(queueUrl: NetStandardTestHelper.TestValues.StringEmpty));

            TestHelper.AssertArgumentNullException(ex,
                "queueUrl");
        }

        /// <summary>
        /// Test that function receives all messages
        /// </summary>
        [Fact]
        public async void ReceiveAllMessagesAsync()
        {
            var response = await TestValues.SQSHelper_Mock.ReceiveAllMessagesAsync(queueUrl: TestValues.QueueName);

            Assert.NotNull(response);
            _ = Assert.Single(response);
        }

        #endregion ReceiveAllMessagesAsync

        #region ReceiveMessagesAsync

        /// <summary>
        /// Test that function throws on null queue url
        /// </summary>
        [Fact]
        public async void ReceiveMessagesAsync_Null_QueueUrl()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.ReceiveMessagesAsync(queueUrl: NetStandardTestHelper.TestValues.StringEmpty));

            TestHelper.AssertArgumentNullException(ex,
                "queueUrl");
        }

        /// <summary>
        /// Test that function receives messages
        /// </summary>
        [Fact]
        public async void ReceiveMessagesAsync()
        {
            var response = await TestValues.SQSHelper_Mock.ReceiveMessagesAsync(queueUrl: TestValues.QueueName);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK,
                response.HttpStatusCode);
        }

        #endregion ReceiveMessagesAsync

        #region SendMessageAsync

        /// <summary>
        /// Test that function throws on null queue url
        /// </summary>
        [Fact]
        public async void SendMessageAsync_Null_QueueUrl()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.SendMessageAsync(queueUrl: NetStandardTestHelper.TestValues.StringEmpty,
                messageBody: TestValues.Body));

            TestHelper.AssertArgumentNullException(ex,
                "queueUrl");
        }

        /// <summary>
        /// Test that function throws on null message body
        /// </summary>
        [Fact]
        public async void SendMessageAsync_Null_MessageBody()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.SendMessageAsync(queueUrl: TestValues.QueueUrl,
                messageBody: NetStandardTestHelper.TestValues.StringEmpty));

            TestHelper.AssertArgumentNullException(ex,
                "messageBody");
        }

        /// <summary>
        /// Test that function receives messages
        /// </summary>
        [Fact]
        public async void SendMessageAsync()
        {
            var response = await TestValues.SQSHelper_Mock.SendMessageAsync(queueUrl: TestValues.QueueName,
                messageBody: TestValues.Body);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK,
                response.HttpStatusCode);
        }

        #endregion SendMessageAsync

        #region SendMessagesAsync

        /// <summary>
        /// Test that function throws on null queue url
        /// </summary>
        [Fact]
        public async void SendMessagesAsync_Null_QueueUrl()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.SendMessagesAsync(queueUrl: NetStandardTestHelper.TestValues.StringEmpty,
                messages: new List<SendMessageBatchRequestEntry>()));

            TestHelper.AssertArgumentNullException(ex,
                "queueUrl");
        }

        /// <summary>
        /// Test that function throws on null message body
        /// </summary>
        [Fact]
        public async void SendMessagesAsync_Null_MessageBody()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => TestValues.SQSHelper.SendMessagesAsync(queueUrl: TestValues.QueueUrl,
                messages: null));

            TestHelper.AssertArgumentNullException(ex,
                "messages");
        }

        /// <summary>
        /// Test that function receives messages
        /// </summary>
        [Fact]
        public async void SendMessagesAsync()
        {
            var response = await TestValues.SQSHelper_Mock.SendMessagesAsync(queueUrl: TestValues.QueueName,
                messages: new List<SendMessageBatchRequestEntry>());

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK,
                response.HttpStatusCode);
        }

        #endregion SendMessagesAsync
    }
}