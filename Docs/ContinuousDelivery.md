# Continuous delivery

## Problem

The product owner wants to see improvements as soon as possible, but it is not efficient for the developer to do repetitive actions to make new builds.

Also, it is not easy to run Web build locally for a non-experienced user (so we need some artifact storage).

## Solution

We use automatic build pipeline: builds created by [Github Actions](https://docs.github.com/en/actions) + [game.ci](https://game.ci/) and artifacts then uploads to [Itch.io](https://itch.io) using [Butler](https://itch.io/docs/butler).

It also improves project maintainability - integration issues (like 'build started to fail') will be highlighted as soon as they happen.

## Alternatives

We can use [TeamCity](https://www.jetbrains.com/teamcity/), [Jenkins](), or other systems, but GitHub is a simpler solution for project requirements.

## Hints

To check available Unity versions for [game.ci](https://game.ci/) navigates to https://hub.docker.com/r/unityci/editor/tags and lookup corresponding version tag.