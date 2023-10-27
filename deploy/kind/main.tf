terraform {
  required_providers {
    kind = {
      source = "tehcyx/kind"
      version = "0.2.1"
    }
  }
}

provider "kind" {}

resource "kind_cluster" "cluster" {
  name = "test-cluster"

  kind_config {
    api_version = "kind.x-k8s.io/v1alpha4"
    kind        = "Cluster"
    node {
      role = "control-plane"
      extra_port_mappings {
        container_port = 9090
        host_port      = 9090
      }
      extra_port_mappings {
        container_port = 3000
        host_port      = 3000
      }
    }
  }
}

output "client_certificate" {
  value = kind_cluster.cluster.client_certificate
}

output "client_key" {
  value = kind_cluster.cluster.client_key
}

output "cluster_ca_certificate" {
  value = kind_cluster.cluster.cluster_ca_certificate
}

output "endpoint" {
  value = kind_cluster.cluster.endpoint
}