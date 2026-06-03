# TeamCityMayor

JetBrains TeamCity CI/CD 시스템을 관리하는 WPF 데스크톱 애플리케이션.

## 기술 스택

| | |
|--|--|
| 언어 / 플랫폼 | C# / .NET 9.0 Windows |
| UI | WPF + WPF-UI |
| MVVM | Prism (Unity DI) + ReactiveUI |
| API | TeamCityAPI NuGet |
| 직렬화 | Newtonsoft.Json |

## 구조

```
TeamCityMayor/
├── Models/           # Agent, AgentCollection, BuildConfiguration, BuildConfigurationCollection
├── Views/            # AgentsView, BuildConfigurationsView (+ ViewModel)
├── Services/         # TeamCityManager (REST API 통신)
├── Bootstrapper.cs   # Prism DI 설정
└── ShellViewModel.cs # 주 윈도우 ViewModel
```

## 기능

- TeamCity 에이전트 목록 조회 및 상태 확인
- 빌드 구성(Build Configuration) 조회 및 관리
- REST API 기반 실시간 연동
