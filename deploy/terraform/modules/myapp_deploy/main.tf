terraform {
  required_providers {
    helm = {
      source = "hashicorp/helm"
    }
  }
}

resource "helm_release" "myapp" {
  chart = "./chart/"
  name = "myapp"
}