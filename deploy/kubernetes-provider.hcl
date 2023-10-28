dependency "kind" {
  config_path = "${path_relative_from_include()}/kind"
}

inputs = {
  __client_certificate = dependency.kind.outputs.client_certificate
  __client_key = dependency.kind.outputs.client_key
  __cluster_ca_certificate = dependency.kind.outputs.cluster_ca_certificate
  __endpoint = dependency.kind.outputs.endpoint
}

generate "provider" {
  path = "kube-provider.generated.tf"
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