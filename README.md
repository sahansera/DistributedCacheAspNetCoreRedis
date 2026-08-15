# Distributed Caching on .NET with IDistributedCache

[![.NET](https://github.com/sahansera/DistributedCacheAspNetCoreRedis/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/sahansera/DistributedCacheAspNetCoreRedis/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/sahansera/DistributedCacheAspNetCoreRedis/actions/workflows/codeql.yml/badge.svg?branch=main)](https://github.com/sahansera/DistributedCacheAspNetCoreRedis/actions/workflows/codeql.yml)
[![Twitter: _SahanSera](https://img.shields.io/twitter/follow/_SahanSera.svg?style=social)](https://twitter.com/_SahanSera)

## Intro 👋

This project uses ASP.NET Core's `IDistributedCache` abstraction with Redis to share cached data between application instances. If you only need a cache inside one application process, my [in-memory caching project](https://github.com/sahansera/InMemoryCacheNetCore) is a better starting point.

I've also [blogged](https://sahansera.dev/distributed-caching-aspnet-core-redis/) this with a full explanation of how this is achieved.

`main` targets .NET 10 LTS. The unsupported .NET 5 version remains available on the [`dotnet5` branch](https://github.com/sahansera/DistributedCacheAspNetCoreRedis/tree/dotnet5) for historical reference.

## Architecture 🏗

![](https://sahansera.dev/static/f5cf079e725b11c30eb666b3ff414626/d7ceb/distributed-caching-in-aspdotnet-core-with-redis-1.png)

1. User requests a user object.
2. App server checks if we already have a user in the cache and return the object if present.
3. App server makes a HTTP call to retrieve the list of users.
4. Users service returns the users list to the app server.
5. App server sends the users list to the distributed (Redis) cache.
6. App server gets the cached version until it expires (TTL).
7. User gets the cached user object.

## Usage 🚀

Requirements:

- .NET 10 SDK
- Docker with Docker Compose

Start Redis:

```sh
docker compose up -d
```

Run the application:

```sh
dotnet run --project DistributedCache/DistributedCache.csproj
```

Build and test the solution:

```sh
dotnet test
```

## Questions? Bugs? Suggestions for Improvement? ❓

Having any issues or troubles getting started? [Get in touch with me](https://sahansera.dev/contact/) 

## Support 🎗

Has this Project helped you learn something new? or helped you at work? Please consider giving a ⭐️ if this project helped you!

## Share it! ❤️

Please share this Repository within your developer community, if you think that this would make a difference! Cheers.

## Contributing ✍️

PRs are welcome! Thank you
