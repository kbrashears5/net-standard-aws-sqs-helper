using Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWSSQSHelper.Test
{
    /// <summary>
    /// Test values for the <see cref="AWSSQSHelper.Test"/> namespace
    /// </summary>
    internal static class TestValues
    {
        internal static ILogger Logger { get; } = new ConsoleLogger(logLevel: LogLevel.Off,
            logName: "Log");

        internal static ISQSHelper SQSHelper { get; } = new SQSHelper(logger: Logger);

        internal static ISQSHelper SQSHelper_Mock { get; } = new SQSHelper_Mock();

        internal static string QueueName { get; } = nameof(QueueName);

        internal static string QueueUrl { get; } = nameof(QueueUrl);

        internal static string ReceiptHandle { get; } = nameof(ReceiptHandle);

        internal static string Body { get; } = nameof(Body);
    }
}