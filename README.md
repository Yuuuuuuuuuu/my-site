# Go Practice 🚀

Go 练手项目 — 前后端分离的 monorepo。

## 项目结构

```
├── backend/          # Go API 后端
│   ├── cmd/server/   # 入口
│   └── internal/     # 内部包（handler / model / service）
├── frontend/         # React + Vite 前端
└── .github/          # CI/CD 工作流
```

## 本地运行

### 后端

```bash
cd backend
go mod tidy
go run ./cmd/server
```

API 服务启动在 `http://localhost:8080`

### 前端

```bash
cd frontend
npm install
npm run dev
```

前端开发服务器启动在 `http://localhost:5173`，API 请求自动代理到后端。
