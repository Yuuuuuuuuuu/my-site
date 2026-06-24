# Go Practice 🚀

练手项目 — 前后端分离架构。

## 架构

```
├── backend/               # .NET 8 Web API
│   └── GoPractice.Api/    # Controllers / Models / EF Core + MySQL
├── frontend/              # React + Vite（可选换其他框架）
└── .github/workflows/     # CI/CD 自动部署
```

## 本地运行

### 后端

需要 .NET 8 SDK 和 MySQL。

```bash
cd backend/GoPractice.Api
# 修改 appsettings.Development.json 里的数据库连接串
dotnet run
```

API 服务启动在 `http://localhost:8080`

### 前端

```bash
cd frontend
npm install
npm run dev
```

开发服务器 `http://localhost:5173`，`/api` 请求自动代理到后端。

## 部署

推送 `main` 分支 → GitHub Actions 自动构建 → SCP 部署到服务器。
