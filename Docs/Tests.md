# Tests

## Problems

- **Manual test efforts** - to make sure your system works as expected you need to test it manually (by yourself or using QA specialists)
- **Long feedback loop** - to get feedback you need to go through "implement -> commit -> build -> test -> describe" loop, which can be really long in time
- **Regression issues** - you need to re-test old things to make sure that you don't introduce new issues by refactoring or minor changes in connected areas

## Solution

As a partial solution of problems above - automatic tests approach (unit/integration/end-to-end, etc). They allow you to reduce feedback loop and regression chances.

## Unit tests

In Unity, you can use - [NUnit](https://nunit.org) as a test framework and [Unity Test Runner](https://docs.unity3d.com/2017.4/Documentation/Manual/testing-editortestsrunner.html) as a UI to run tests.

## Test coverage

To make sure that your tests are solid and reliable you can use coverage metrics - how many lines of code are executed in a test run.

Use it with caution, it's easy to write a test with high coverage but no actual assets (formally nice but doesn't make sense).

Unity provides [Code Coverage](https://docs.unity3d.com/Packages/com.unity.testtools.codecoverage@1.1/manual/CoverageTestRunner.html) to generate coverage reports.

You can see the test coverage example [here](../UnityClient/TestCoverage/Report/index.html).