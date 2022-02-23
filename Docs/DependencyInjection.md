# Dependency injection

## Problems

- Complex parts of a system uses many dependencies
- To add a new dependency in the low-level component you need to pass it through all usage tree
- It can be hard to use dependencies in [tests](Tests.md)

## Solution

[VContainer](https://github.com/hadashiA/VContainer) is a modern DI framework for Unity that provides the basic feature set and adequate performance.

## Alternatives

- [Zenject](https://github.com/Mathijs-Bakker/Extenject) - provides more features but includes more hidden logic and has performance issues
- **Service locator pattern** - dependencies can be accessed in any place (which is harder to read, debug & test), commonly considered as anti-pattern