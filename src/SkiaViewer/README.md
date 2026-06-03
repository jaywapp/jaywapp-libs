# SkiaViewer

SkiaSharp 기반 대화형 2D 그래픽 뷰어. 레이어 관리, Pan/Zoom, 기하학 변환을 지원하는 WPF 애플리케이션.

## 기술 스택

| | |
|--|--|
| 언어 / 플랫폼 | C# / .NET Framework 4.7.2 Windows |
| UI | WPF |
| 렌더링 | SkiaSharp |
| MVVM | ReactiveUI |
| 파일 다이얼로그 | Ookii.Dialogs |

## 구조

```
SkiaViewer/
├── SkiaViewer/                       # 메인 애플리케이션 (MainWindow, LayersView)
├── Pentacube.Manufacture.Canvas/     # SkiaSharp 캔버스 컨트롤
│   ├── Service/                      # SKDrawer, PanAndZoomEventWatcher, ReactivePanZoom
│   └── Event/                        # Flip, Rotate, Offset 이벤트
└── Pentacube.Manufacture.Canvas.Geometry/  # 기하학 객체 및 변환
    └── Model/Layer.cs                # 레이어 관리
```

## 기능

- SkiaSharp 기반 벡터 그래픽 렌더링
- 마우스 드래그 Pan / 휠 Zoom
- 레이어 추가·숨기기·순서 변경
- 도형 회전, 반전, 오프셋 변환
