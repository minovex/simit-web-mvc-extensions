# Simit.Web.Mvc.Extensions

This library contains MVC html helpers methods.

## Html Extensions

@Html.CSS(params string[] cssPath)

@Html.JS(params string[] jsPath)

@Html.IMG(string path, string alt = null, object attiributes = null)


## Configuring CDN

To configure the Extension, an <appSettings> element should be included in the program's Web.config file.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="simit:cdn:url" value="http://domain..." />
	
```




## Localization Extensions

@Html.LocalResource(string key)

@Html.LocalResource(string key, object replacements)


## Nuget


[Nuget Package on Nuget.org](https://www.nuget.org/packages/Simit.Web.Mvc.Extensions/)
