# BetStream DevOps Project

BetStream is a .NET 8 API packaged for Docker, deployable with raw Kubernetes manifests or Helm, observable with Prometheus, and installable into an existing cluster through Terraform.

## What This Repo Covers

- ASP.NET Core API with Prometheus metrics and readiness/liveness endpoints
- Docker Compose stack for local API, PostgreSQL, Kafka, Prometheus, and Grafana
- Kubernetes manifests with config separation and health probes
- Helm chart with configurable image, runtime config, secrets, and resources
- GitHub Actions for build, schema bootstrap, test, compose validation, container build, and security scans
- Terraform to install the Helm chart into an existing Kubernetes cluster

## Prerequisites

- Docker and Docker Compose
- .NET 8 SDK
- kubectl and a Kubernetes cluster such as Minikube, kind, or Docker Desktop Kubernetes
- Helm 3
- Terraform 1.6+

## Local Development

Run the full stack:

```bash
docker compose up --build
```

Endpoints:

- API: [http://localhost:8080](http://localhost:8080)
- Swagger: [http://localhost:8080/swagger](http://localhost:8080/swagger)
- Live health: [http://localhost:8080/health/live](http://localhost:8080/health/live)
- Ready health: [http://localhost:8080/health/ready](http://localhost:8080/health/ready)
- Metrics: [http://localhost:8080/metrics](http://localhost:8080/metrics)
- Prometheus: [http://localhost:9090](http://localhost:9090)
- Grafana: [http://localhost:3000](http://localhost:3000)

Default Grafana login:

- Username: `admin`
- Password: `admin`

## Kubernetes Manifests

Apply the manifests:

```bash
kubectl apply -f k8s/
```

Port-forward the service:

```bash
kubectl port-forward svc/betstream-service 8080:80
```

The manifests expect supporting services such as PostgreSQL and Kafka to already exist in the cluster and expose the hostnames used in `k8s/configmap.yaml`.

## Helm

Install the chart:

```bash
helm upgrade --install betstream ./betstream-chart
```

Override the image at deploy time:

```bash
helm upgrade --install betstream ./betstream-chart \
  --set image.repository=ghcr.io/your-org/betstream \
  --set image.tag=1.0.0
```

## Terraform

Terraform deploys the Helm chart into an existing Kubernetes cluster:

```bash
cd terraform
terraform init
terraform plan -var="image_repository=ghcr.io/your-org/betstream" -var="image_tag=1.0.0"
terraform apply -var="image_repository=ghcr.io/your-org/betstream" -var="image_tag=1.0.0"
```

## CI/CD

`/.github/workflows/ci-cd.yml` currently validates:

- .NET restore, build, and test
- PostgreSQL schema bootstrap using `schema.sql`
- Docker Compose configuration
- Docker image build

`/.github/workflows/codeql.yml` runs CodeQL, Trivy, and Gitleaks for code and supply-chain hygiene.

## Notes

- Runtime configuration is environment-variable driven for database, Kafka, and JWT settings.
- The Kubernetes and Helm examples include placeholder values for secrets and image repository; replace them before deploying beyond local/demo environments.
