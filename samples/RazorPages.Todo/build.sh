#!/bin/bash

#Need $(PWD) to work around including files bug that hasn't been fixed yet.
dotnet publish ./src/Todo.Service -o $(PWD)/output/Service
dotnet publish ./src/Todo.Web -o $(PWD)/output/Web
docker-compose build