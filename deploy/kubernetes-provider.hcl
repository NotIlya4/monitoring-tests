dependency "kubeconfig" {
  config_path = "${path_relative_from_include()}/kubeconfig"
}

inputs = {
  __client_certificate = dependency.kubeconfig.outputs.client_certificate
  __client_key = dependency.kubeconfig.outputs.client_key
  __cluster_ca_certificate = dependency.kubeconfig.outputs.cluster_ca_certificate
  __endpoint = dependency.kubeconfig.outputs.endpoint
}

generate "provider" {
  path = "__kube-provider.tf"
  if_exists = "overwrite"
  contents = <<EOF
provider "kubernetes" {
  host = var.__endpoint

  client_certificate     = var.__client_certificate
  client_key             = var.__client_key
  cluster_ca_certificate = var.__cluster_ca_certificate
}

provider "helm" {
  kubernetes {
    host = var.__endpoint
    
    client_certificate     = var.__client_certificate
    client_key             = var.__client_key
    cluster_ca_certificate = var.__cluster_ca_certificate
  }
}

variable "__client_certificate" {
  type = string
}

variable "__client_key" {
  type = string
}

variable "__cluster_ca_certificate" {
  type = string
}

variable "__endpoint" {
  type = string
}
EOF
}