ReversePackageSearch
====================

Search types and members to find which NuGet packages they are in.

This is a little project I built in a few nights to help find packages from types.


Using
====================

You will need the ASP.NET vNext tools setup: http://www.github.com/aspnet/Home

Restore all the packages, from inside the ReversePackageSearch folder (where the project.json is)
```kpm restore```
```npm install```
```bower install```

Use grunt to transpile the site.less files and copy stuff from the bower install directory to a Content directory. The site expects everything to be in a directory called content.

```grunt dev```
