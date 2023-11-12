variable "kubeconfig_path" {
  type = string
}

variable "context" {
  type = string
}

output "kubeconfig_path" {
  value = var.kubeconfig_path
}

output "context" {
  value = var.context
}