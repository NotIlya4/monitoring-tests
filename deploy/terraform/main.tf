variable "config_context" {
  type = string
  default = "minikube"
}

variable "config_path" {
  type = string
  default = ""
}

locals {
  config_path = var.config_path == "" ? module.create_kubeconfig_file.file_path : var.config_path
}

module "create_kubeconfig_file" {
  source = "./modules/create_kubeconfig_file"
}

provider "kubernetes" {
  alias = "kubernetes"
  config_path    = local.config_path
  config_context = var.config_context
}

provider "helm" {
  alias = "helm"
  kubernetes {
    config_path = local.config_path
    config_context = var.config_context
  }
}

provider "git" {}

module "kube_prometheus_stack" {
  source = "./modules/kube_prometheus_stack"
  
  depends_on = [module.create_kubeconfig_file]
  
  providers = {
    helm = helm.helm
  }
}