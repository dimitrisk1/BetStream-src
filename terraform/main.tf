terraform {
  required_version = ">= 1.6.0"

  required_providers {
    helm = {
      source  = "hashicorp/helm"
      version = "~> 2.14"
    }
    kubernetes = {
      source  = "hashicorp/kubernetes"
      version = "~> 2.31"
    }
  }
}

variable "kubeconfig_path" {
  description = "Path to a kubeconfig file for the target cluster."
  type        = string
  default     = "~/.kube/config"
}

variable "namespace" {
  description = "Namespace where BetStream will be deployed."
  type        = string
  default     = "betstream"
}

variable "image_repository" {
  description = "Container image repository for the BetStream API."
  type        = string
  default     = "ghcr.io/example/betstream"
}

variable "image_tag" {
  description = "Container image tag for the BetStream API."
  type        = string
  default     = "latest"
}

provider "kubernetes" {
  config_path = var.kubeconfig_path
}

provider "helm" {
  kubernetes {
    config_path = var.kubeconfig_path
  }
}

resource "kubernetes_namespace" "betstream" {
  metadata {
    name = var.namespace
  }
}

resource "helm_release" "betstream" {
  name             = "betstream"
  namespace        = kubernetes_namespace.betstream.metadata[0].name
  create_namespace = false
  chart            = "${path.module}/../betstream-chart"

  values = [
    yamlencode({
      image = {
        repository = var.image_repository
        tag        = var.image_tag
      }
    })
  ]
}

output "namespace" {
  value = kubernetes_namespace.betstream.metadata[0].name
}

output "release_name" {
  value = helm_release.betstream.name
}
