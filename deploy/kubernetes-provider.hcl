dependency "minikube" {
  config_path = "${path_relative_from_include()}/minikube"
}

inputs = {
  __path = dependency.minikube.outputs.kubeconfig_path
  __context = dependency.minikube.outputs.context
}

generate "provider" {
  path = "kube-provider.generated.tf"
  if_exists = "overwrite"
  contents = <<EOF
provider "kubernetes" {
  config_path    = var.__path
  config_context = var.__context
}

provider "helm" {
  kubernetes {
    config_path    = var.__path
    config_context = var.__context
  }
}

variable "__path" {
  type = string
}

variable "__context" {
  type = string
}
EOF
}

#generate "provider" {
#  path = "kube-provider.generated.tf"
#  if_exists = "overwrite"
#  contents = <<EOF
#provider "kubernetes" {
#  host = var.__endpoint
#
#  client_certificate     = var.__client_certificate
#  client_key             = var.__client_key
#  cluster_ca_certificate = var.__cluster_ca_certificate
#}
#
#provider "helm" {
#  kubernetes {
#    host = var.__endpoint
#    
#    client_certificate     = var.__client_certificate
#    client_key             = var.__client_key
#    cluster_ca_certificate = var.__cluster_ca_certificate
#  }
#}
#
#variable "__client_certificate" {
#  type = string
#}
#
#variable "__client_key" {
#  type = string
#}
#
#variable "__cluster_ca_certificate" {
#  type = string
#}
#
#variable "__endpoint" {
#  type = string
#}
#EOF
#}