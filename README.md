# Distributed Caching on .NET with IDistributedCache

[![Documentation](https://img.shields.io/badge/documentation-yes-brightgreen.svg)](sahansera.dev)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](#)
[![Twitter: _SahanSera](https://img.shields.io/twitter/follow/_SahanSera.svg?style=social)](https://twitter.com/_SahanSera)

## Intro 👋

This project leverages the IDistributedCache that's shipped as part of .NET/.NET Core SDKs to achieve distributed caching in a microservices environment. If you are looking for a monolithing caching approach, then, my [other project](https://github.com/sahansera/InMemoryCacheNetCore) would be more suitable for you.

I've also [blogged](https://sahansera.dev/distributed-caching-aspnet-core-redis/) this with a full explanation of how this is achieved.

> Note: I have recently migrated this project to .NET 6 and also a docker-compose.yaml for better dev experience 🎉 You can still access the old version from the [.NET 5 branch](https://github.com/sahansera/DistributedCacheAspNetCoreRedis/tree/dotnet5) in this repo. 

## Architecture

![](https://sahansera.dev/static/f5cf079e725b11c30eb666b3ff414626/d7ceb/distributed-caching-in-aspdotnet-core-with-redis-1.png)

1. User requests a user object.
2. App server checks if we already have a user in the cache and return the object if present.
3. App server makes a HTTP call to retrieve the list of users.
4. Users service returns the users list to the app server.
5. App server sends the users list to the distributed (Redis) cache.
6. App server gets the cached version until it expires (TTL).
7. User gets the cached user object.

## Usage 🚀

Open up a terminal and run the following:

```sh
docker-compose up and dotnet run
```

## Questions? Bugs? Suggestions for Improvement? ❓

Having any issues or troubles getting started? [Get in touch with me](https://sahansera.dev/contact/) 

## Support 🎗

Has this Project helped you learn something new? or helped you at work? Please consider giving a ⭐️ if this project helped you!

or Give a ⭐️ if this project helped you!

## Share it! ❤️

Please share this Repository within your developer community, if you think that this would make a difference! Cheers.

## Contributing ✍️

PRs are welcome! Thank you
