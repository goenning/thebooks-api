#!/bin/bash

if [ "$1" == "-w" ] ; then
	(cd src/TheBooks.Api/; dotnet watch run)
else
  dotnet run --project src/TheBooks.Api/project.json
fi 