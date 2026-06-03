# Jaywapp.API

ASP.NET Core 8.0 기반 웹 API 스캐폴딩 프로젝트.

## 기술 스택

| | |
|--|--|
| 언어 / 플랫폼 | C# / .NET 8.0 |
| 프레임워크 | ASP.NET Core Web API |
| 문서화 | Swagger / OpenAPI (Swashbuckle) |

## 구조

```
Jaywapp.API/
├── Controllers/WeatherForecastController  # 샘플 API 엔드포인트
├── WeatherForecast.cs                     # 데이터 모델
└── Program.cs                             # 최소 호스팅 모델 설정
```

## 실행

```bash
dotnet run
```

Swagger UI: `http://localhost:{port}/swagger`
