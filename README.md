[![|](https://i.ibb.co/8XF6SKd/angular.png)](https://github.com/OpenLeafProject/AngularLeaf)&emsp;[![|](https://i.ibb.co/Ws5JfT7/NetCore.png)](https://github.com/OpenLeafProject/CoreLeaf)

# Leaf (REST API) 

[![Build Status](https://img.shields.io/badge/build-passing-brightgreensvg?style=flat)](https://github.com/Ukkime/Leaf) [![contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/Ukkime/Leaf/issues)


### What is Leaf?
Leaf is a complete solution for clinical data management with this awesome features
  - Patients management
  - Scheduled dates management
  - Invoices management
  - Patients history management


### Tech

Leaf uses a number of open source projects to work properly:

* [Angular] - HTML enhanced for web apps!
* [.NET Core] - powerfull APIREST build with .NET Core 3.1
* [Twitter Bootstrap] - great UI boilerplate for modern web apps
* [Material Design] - beautifull components for a beautifull web app

### Leaf REST API Installation

[TO DO: WRITE COMMANDS TO RESTORE PACKAGES - INSTALL LEAF]

```sh
$ cd leaf
$ dotnet restore
```

### Docker
Leaf is very easy to install and deploy in a Docker container.

By default, the Docker will expose port 8080, so change this within the Dockerfile if necessary. When ready, simply use the Dockerfile to build the image.

```sh
cd leaf
docker build -t [TO DO]
```

