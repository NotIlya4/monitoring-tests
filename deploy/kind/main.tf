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