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
  config_context = var.config_context
}