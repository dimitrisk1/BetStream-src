# 🚀 BetStream DevOps Project

## 📦 Overview

End-to-end DevOps project using:

* ASP.NET Core
* Docker
* Kubernetes
* Terraform
* Prometheus & Grafana

---

# ⚙️ Prerequisites

Make sure you have installed:

* Docker
* Kubernetes (Docker Desktop / Minikube)
* kubectl
* Terraform
* Azure CLI (optional for cloud)

---

# 🐳 1. Build Docker Image

```bash
docker build -t betstream-app ./app
```

---

# 🐳 2. Run Locally (Docker Compose)

```bash
docker compose up --build
```

👉 App:

```
http://localhost:8080
```

---

# ☸️ 3. Run in Kubernetes

## Apply manifests

```bash
kubectl apply -f k8s/
```

## Check pods

```bash
kubectl get pods
```

## Check services

```bash
kubectl get svc
```

---

# 🌐 4. Access Application

## Option A (port-forward)

```bash
kubectl port-forward svc/betstream-service 8080:80
```

👉 Open:

```
http://localhost:8080
```

---

# 📊 5. Metrics Endpoint

```bash
http://localhost:8080/metrics
```

---

# 📈 6. Monitoring (Prometheus + Grafana)

## Run monitoring stack

```bash
docker compose up prometheus grafana
```

## Access Grafana

```
http://localhost:3000
```

👉 Default login:

* user: admin
* password: admin

---

# 📝 7. Logs (Loki)

```bash
docker compose up loki promtail
```

👉 Add Loki in Grafana:

```
http://loki:3100
```

---

# ☁️ 8. Infrastructure (Terraform)

```bash
cd terraform

terraform init
terraform plan
terraform apply
```

---

# 🔁 9. CI/CD (GitHub Actions)

Pipeline automatically:

* Builds application
* Runs tests
* Builds Docker image
* Pushes to registry
* Deploys to Kubernetes

---

# 🧪 10. Run Tests Locally

```bash
dotnet test
```

---

# 🗄️ 11. Run Database Migrations

```bash
dotnet ef database update -p BetStream/BetStream.csproj -s BetStream/BetStream.csproj
```

---

# 🧹 12. Cleanup

```bash
kubectl delete -f k8s/
docker compose down
```

---

# 💡 Architecture Flow

```
Terraform → Azure → Kubernetes → Docker → ASP.NET App
                         ↓
                Prometheus / Grafana / Loki
```

---

# 🚀 Author

Jim 
