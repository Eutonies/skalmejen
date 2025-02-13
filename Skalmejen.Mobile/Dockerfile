FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet publish Skalmejen.Mobile/Skalmejen.Mobile.csproj -c Release -o output


FROM nginx
WORKDIR /var/www/web
COPY --from=build /app/output/wwwroot .
COPY Skalmejen.Mobile/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80