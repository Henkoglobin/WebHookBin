# WebHookBin

WebHookBin provides an easy way to log data sent by [Webhooks](https://en.wikipedia.org/wiki/Webhook). Simply set-up any webhook with the URL of your instance of WebHookBin and watch the data coming in!

## Setup

WebHookBin is an ASP.NET application, using .NET 6. If you want to build it locally, you'll need to [install the .NET 6 SDK](https://docs.microsoft.com/en-us/dotnet/core/install/) for your platform. Afterwards, run: 

```bash
dotnet build --configuration Release WebHookBin
cd WebHookBin/bin/Release/net6.0
ASPNETCORE_URLS="http://localhost:5000" dotnet WebHookBin.dll
```

This will start WebHookBin on [localhost:5000](http://localhost:5000).

### Running with Docker

WebHookBin can also easily be started in a docker container:

```bash
docker run -ti -p 5000:80 --name WebHookBin --rm ghcr.io/henkoglobin/webhookbin:main
```

This will start WebHookBin in Docker and bind it to [localhost:5000](http://localhost:5000).

### Reverse Proxy

WebHookBin is intended to be run behind a [Reverse Proxy](https://en.wikipedia.org/wiki/Reverse_proxy), which should also handle authentication. For Apache 2.4, your configuration may look like this:

```ApacheConf
<VirtualHost *:443>
    ServerName bin.example.com

    ProxyPreserveHost On
    AllowEncodedSlashes Off

    ProxyPass "/" "http://localhost:5000"
    ProxyPassReverse / https://bin.example.com/

    <Location /api>
        Order deny,allow
        Allow from all
        Satisfy any
    </Location>

    <Location />
        AuthType Basic
        AuthName "Request Log UI"
        AuthBasicProvider file
        AuthUserFile /var/apache2/webhook-bin-passwd
        Require valid-user
    </Location>

    RequestHeader set "X-Forwarded-Proto" expr=%{REQUEST_SCHEME}

    # ...
</VirtualHost>
```

