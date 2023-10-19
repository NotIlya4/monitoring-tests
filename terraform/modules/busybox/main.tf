terraform {
  required_providers {
    kubernetes = {
      source = "hashicorp/kubernetes"
    }
  }
}

variable "name" {
  type = string
  default = "busybox"
}

resource "kubernetes_pod_v1" "busybox" {
  metadata {
    name = "busybox"
  }
  spec {
    container {
      name = var.name
      image = "busybox:latest"
      command = ["sleep", 3600]
      image_pull_policy = "Always"
    }
    restart_policy = "Always"
  }
}