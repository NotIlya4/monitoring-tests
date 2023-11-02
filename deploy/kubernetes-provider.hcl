dependency "kubeconfig" {
  config_path = "${path_relative_from_include()}/kubeconfig"
}

inputs = {
  __path = dependency.kubeconfig.outputs.kubeconfig_path
  __context = dependency.kubeconfig.outputs.context
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