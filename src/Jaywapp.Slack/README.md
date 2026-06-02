# Jaywapp.Slack

Slack Web API(Blocks)를 간단히 사용할 수 있게 도와주는 .NET 라이브러리입니다. 블록 컴포넌트를 손쉽게 조합하여 메시지를 만들고, `chat.postMessage`로 채널에 전송할 수 있습니다.

- Target Framework: `netstandard2.1`
- Dependencies: `Newtonsoft.Json`

## 설치

NuGet 패키지(게시 완료 후 사용 가능):

```
dotnet add package Jaywapp.Slack
```

또는 패키지 관리자 콘솔:

```
Install-Package Jaywapp.Slack
```

## 빠른 시작

1) Slack 앱에서 Bot Token을 발급하고 `chat:write` 권한을 추가하세요.
2) 채널 ID(예: `C0123456789`)를 확인하세요.
3) 아래 예시처럼 메시지를 생성하고 전송합니다.

```csharp
using Jaywapp.Slack.Services;
using Jaywapp.Slack.Models.Messages;
using Jaywapp.Slack;

// 1) 메시지 구성
var builder = SlackMessageBuilder.Create()
    .AddHeader("배포 알림")
    .AddSection("새 버전이 배포되었습니다 :tada:")
    .AddDivider()
    .AddSectionFields(
        "*버전*: 0.1.3",
        "*상태*: 성공"
    )
    .AddButtons(
        (text: "확인", actionId: "ack", value: "ok", emoji: true),
        (text: "자세히", actionId: "detail", value: "link", emoji: true)
    );

var message = builder.Build();

// 2) 전송
var token = "xoxb-...";            // Slack Bot Token
var channel = "C0123456789";       // 채널 ID
var slack = new SlackService(token);
var ok = await slack.Send(channel, message);
```

> 참고: Slack의 `chat.postMessage`는 채널 **이름**이 아니라 **채널 ID** 사용을 권장합니다.

## 주요 기능

- 블록 빌더(`SlackMessageBuilder`)
  - `AddHeader(string text, bool emoji = true)`
  - `AddSection(string text, SlackTextType type = SlackTextType.Mrkdwn)`
  - `AddSectionFields(params string[] fields)`
  - `AddDivider()`
  - `AddImage(string imageUrl, string altText)`
  - `AddButton(string text, string actionId, string value = null, bool emoji = true)`
  - `AddButtons(params (string text, string actionId, string value, bool emoji)[] buttons)`
- 전송 서비스(`SlackService`)
  - `Task<bool> Send(string channel, SlackMessage message)`
  - 내부적으로 `https://slack.com/api/chat.postMessage` 호출

## 모델 개요

- Blocks: `SlackSectionBlock`, `SlackDividerBlock`, `SlackImageBlock`, `SlackHeaderBlock`, `SlackActionsBlock`
- Elements: `SlackButtonElement`
- Texts: `SlackTextObject`(`PlainText`, `Mrkdwn` 지원)
- Message: `SlackMessage` (Block 리스트)

## 예시: JSON 출력

빌드된 메시지를 JSON으로 확인하려면:

```csharp
var json = builder.BuildJson(indented: true);
Console.WriteLine(json);
```

## 주의사항

- 실패 시 `false`를 반환합니다. Slack API 응답이나 네트워크 오류 등은 호출 측에서 로깅/재시도 로직을 추가하세요.
- 널 허용 경고(CS8618 등)가 일부 모델에 존재합니다. 사용 시 필요한 필드를 반드시 채워주세요.

## 라이선스

MIT (패키지 메타데이터에 `PackageLicenseExpression` = MIT 설정)
