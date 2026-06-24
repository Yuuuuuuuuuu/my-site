# GoPractice Backend Skeleton

这是一个面向快速复用的 API 骨架，目标是保留熟悉的开发习惯，同时使用较新的运行时栈：

- `.NET 8`
- `MySQL`
- `SqlSugar`
- 分层结构

## 目录说明

- `GoPractice.Api`: Web API 宿主层，只负责路由、中间件、Swagger、依赖注入入口
- `GoPractice.Application`: 用例编排层，放服务、DTO、接口
- `GoPractice.Domain`: 领域实体与公共基类
- `GoPractice.Infrastructure`: `SqlSugar`、仓储实现、数据库初始化
- `GoPractice.Shared`: 通用返回、异常、配置项

## 当前已包含

- 统一返回结构 `ApiResult`
- 全局异常处理中间件
- 请求日志中间件
- `Swagger`
- `SqlSugar + MySQL` 配置
- `JWT` 配置开关与 `Swagger Bearer` 支持
- 当前登录用户上下文 `ICurrentUser`
- 通用仓储基类 `SqlSugarRepository<TEntity>`
- 演示登录服务与 token 颁发接口
- 自动初始化示例表 `demo_records`
- `Demo` 示例模块

## 配置说明

数据库配置在 `GoPractice.Api/appsettings*.json`：

```json
"Database": {
  "DbType": "MySql",
  "ConnectionString": "Server=localhost;Database=go_practice;Uid=root;Pwd=your_password;AllowPublicKeyRetrieval=true;",
  "AutoInitSchema": true
}
```

JWT 配置当前已默认开启，你也可以按需关闭或替换：

```json
"Jwt": {
  "Enabled": true,
  "Issuer": "GoPractice",
  "Audience": "GoPractice.Client",
  "SecretKey": "ReplaceWithAStrongSecretKeyAtLeast32Chars",
  "ExpireMinutes": 120
}
```

演示登录配置如下，默认账号为 `admin / 123456`：

```json
"AuthDemo": {
  "Enabled": true,
  "Users": [
    {
      "UserId": "1",
      "UserName": "admin",
      "Password": "123456",
      "Roles": [ "Admin" ]
    }
  ]
}
```

## 示例接口

- `POST /api/auth/login`: 获取 JWT
- `GET /api/auth/me`: 获取当前登录用户，需要 `Bearer Token`
- `GET /api/demo`: Demo 列表
- `GET /api/demo/{id}`: Demo 详情
- `POST /api/demo`: 创建 Demo 数据
- `PUT /api/demo/{id}`: 更新 Demo 数据
- `DELETE /api/demo/{id}`: 删除 Demo 数据
- `GET /api/demo/secure-ping`: 受保护接口示例

## VS Code 启动

- 请在 VS Code 中直接打开 `backend` 目录
- 已提供 `.vscode/launch.json` 和 `.vscode/tasks.json`
- 启动前需要本机已安装 `.NET 8 SDK`
- 启动方式：
  - 终端执行：`dotnet run --project GoPractice.Api/GoPractice.Api.csproj`
  - 或在 VS Code 中按 `F5`

## 扩展建议

- 新模块优先复用 `BaseApiController`
- 仓储默认继承 `SqlSugarRepository<TEntity>`
- 业务层通过 `ICurrentUser` 获取当前用户信息
- 需要鉴权的控制器或接口直接添加 `[Authorize]`
- 真实项目中建议替换 `DemoAuthService`，不要继续使用明文演示账号
- 新模块可直接参考 [MODULE_TEMPLATE.md](file:///c:/Workplace/GOPRACTICE/go-practice/backend/MODULE_TEMPLATE.md)

## 后续建议

- 增加 Redis 缓存
- 增加审计字段自动填充
- 增加模块化的 `Service + Repository` 模板
- 增加单元测试与集成测试
