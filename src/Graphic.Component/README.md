# Graphic.Component

점, 선, 원, 다각형 등 기하학 도형을 표현하고 연산하는 C# 라이브러리.

## 기술 스택

| | |
|--|--|
| 언어 / 플랫폼 | C# / .NET Core 3.1 |

## 구조

```
Graphic.Component/
├── Geometry/
│   ├── Interface/    # IGraphicComponent, IGraphicFigure, IGraphicTrace
│   ├── Model/        # Point, Line, Circle, Polygon, Rectangle, Curve, Straight
│   └── Operator/     # Calculater (각도, 거리, 길이, 크기)
└── Test/             # 연산 단위 테스트
```

## 기능

- 2D 기하 도형 모델 (점·선·원·다각형·직사각형·곡선)
- 거리, 각도, 길이, 크기 계산 연산자
- 인터페이스 기반 확장 가능 구조
