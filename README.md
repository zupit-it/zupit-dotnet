# Zupit .NET monorepo

This monorepo contains multiple libraries that can be used in .NET applications.

## Libraries Included

- [Zupit.Prometheus](Zupit.Prometheus): this library adds an endpoint to your ASP.NET Core application that returns metrics in the Prometheus format with a token-based authentication.
- [Zupit.VersionEndpoint](Zupit.VersionEndpoint): this library adds a version endpoint to your ASP.NET Core application

## Getting Started

To get started with any of the libraries in this monorepo, please refer to their respective documentation and README files.

## Release

Every library is automatically released to NuGet when pushing to main branch.
The version of the library is calculated by conventional commit. The CHANGELOG.md file is automatically updated with the new version and the changes made.
Under the hood, the version calculation is done by [versionize](https://github.com/versionize/versionize) package.
Supported commit types are:

- `feat: something`: a new minor release is generated (ex. from 1.0.0 to 1.1.0)
- `fix: something`: a new patch release is generated (ex. from 1.0.0 to 1.0.1)
- `feat: something` with description starting with `BREAKING CHANGE`: a new major release is generated (ex. from 1.0.0 to 2.0.0)

For more information about supported commit types, please refer to [versionize](https://github.com/versionize/versionize).

The calculated version is then used to update the version of the library and the version of the package in the .csproj file.
**Each library has its own version and is released separately**.

## Release limitations

Currently, the release process is not perfect and has some limitations, because versionize doesn't support mono-repos.
The limitations are:

- Each commit must contain changes for only one library. If you change multiple libraries in the same commit, multiple release pipelines are triggered, but only the first execution release correctly.
- The CHANGELOG.md file is generated using commits starting from the last release commit (automatically created by pipelines with commit message: `chore(release): {version} [{projectName}]`. So general commits that doesn't trigger a release are included **only** in the CHANGELOG.md file of the next library being released.


## Contributing

These libraries are maintained by the [Zupit](https://www.zupit.it/) team.
If you would like to contribute, please open a pull request with your changes.

### How to add a new library

To add a new library to the monorepo, you can add a new .net solution to the root of the repository and add the library to the solution.
This solution must contain:

- The library project
- A console application that uses the library
- The test project

The library **must contain a README.md** file with the documentation of the library.

Please be aware that due to testing constraints, the current libraries have a specific .NET SDK version set in the global.json file.

To enable the release pipeline for the new library, you must add a new pipeline in the `.github/workflows` folder with filename `release-{libraryName}.yml` that uses the `common-release.yml` template.
Refer to existing release pipelines for examples.