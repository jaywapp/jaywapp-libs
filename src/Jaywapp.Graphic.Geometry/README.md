# Jaywapp.Graphic.Geometry

SkiaSharp 기반 2D 기하학 도형 렌더링 라이브러리. WPF 통합을 지원합니다.

## 기술 스택

| | |
|--|--|
| 언어 / 플랫폼 | C# / .NET Framework 4.7.2 |
| 렌더링 | SkiaSharp + SkiaSharp.Views.WPF |
| UI | WPF (PresentationCore, WindowsBase) |

## 구조

```
Jaywapp.Graphic.Geometry/
├── Model/
│   ├── Base/         # GeometryBase, LineBase, PlaneBase 추상 클래스
│   └── (구현체)      # Arc, Circle, Ellipse, Polygon, Rectangle, Square, Segment
├── Service/Drawer    # 기하학 객체 SkiaSharp 렌더링
├── Helper/SKHelper   # SkiaSharp 유틸리티
└── Event/            # ColorChangeEventArgs, IsVisibleChangeEventArgs
```

## 기능

- 7종 2D 기하 도형 (호·원·타원·다각형·직사각형·정사각형·선분)
- SkiaSharp 기반 고품질 렌더링
- WPF SKElement에 직접 사용 가능
- 색상·가시성 변경 이벤트 지원
