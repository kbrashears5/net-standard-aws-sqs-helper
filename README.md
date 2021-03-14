<h1 align="center">Net Standard AWS SQS Helper</h1>

<div align="center">
    
<b>Collection of helper functions for interacting with the AWS SQS service</b>
    
[![Build Status](https://dev.azure.com/kbrashears5/github/_apis/build/status/kbrashears5.net-standard-aws-sqs-helper?branchName=master)](https://dev.azure.com/kbrashears5/github/_build/latest?definitionId=26&branchName=master)
[![Tests](https://img.shields.io/azure-devops/tests/kbrashears5/github/26)](https://img.shields.io/azure-devops/tests/kbrashears5/github/26)
[![Code Coverage](https://img.shields.io/azure-devops/coverage/kbrashears5/github/26)](https://img.shields.io/azure-devops/coverage/kbrashears5/github/26)

[![nuget](https://img.shields.io/nuget/v/NetStandardAWSSQSHelper.svg)](https://www.nuget.org/packages/NetStandardAWSSQSHelper/)
[![nuget](https://img.shields.io/nuget/dt/NetStandardAWSSQSHelper)](https://img.shields.io/nuget/dt/NetStandardAWSSQSHelper)
</div>

## Usage

### Default - running in Lambda in your own account

```c#
var logger = new ConsoleLogger(logLevel: LogLevel.Information);

var helper = new SQSHelper(logger: logger);

await helper.SendMessageAsync(queueUrl: "https://queue.aws.com",
    messageBody: "message");
```

### Running in separate account or not in Lambda

```c#
var logger = new ConsoleLogger(logLevel: LogLevel.Information);

var options = new AmazonSQSConfig()
{
    RegionEndpoint = RegionEndpoint.USEast1
};

var repository = new AmazonSQSClient(config: options);

var helper = new SQSHelper(logger: logger);

await helper.SendMessageAsync(queueUrl: "https://queue.aws.com",
    messageBody: "message");
```

## Notes

If no options are supplied, will default to `us-east-1` as the region
