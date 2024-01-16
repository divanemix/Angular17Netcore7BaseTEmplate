const { env } = require('process');

const target = "https://localhost:7017"
  //env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:2375';

const PROXY_CONFIG = [
  {
    context: [
      "/api/*",
    ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
