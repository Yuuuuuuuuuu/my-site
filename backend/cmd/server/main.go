package main

import (
	"log"
	"net/http"
	"os"

	"github.com/Yuuuuuuuuuu/go-practice/backend/internal/handler"
)

func main() {
	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}

	mux := http.NewServeMux()

	// Routes
	mux.HandleFunc("GET /api/health", handler.HealthCheck)
	mux.HandleFunc("GET /api/hello", handler.Hello)
	mux.HandleFunc("GET /api/users", handler.GetUsers)
	mux.HandleFunc("POST /api/users", handler.CreateUser)

	// CORS middleware wrapper
	wrapped := corsMiddleware(mux)

	log.Printf("Server starting on :%s", port)
	if err := http.ListenAndServe(":"+port, wrapped); err != nil {
		log.Fatalf("Server failed: %v", err)
	}
}

func corsMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Access-Control-Allow-Origin", "*")
		w.Header().Set("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS")
		w.Header().Set("Access-Control-Allow-Headers", "Content-Type, Authorization")

		if r.Method == http.MethodOptions {
			w.WriteHeader(http.StatusNoContent)
			return
		}

		next.ServeHTTP(w, r)
	})
}
