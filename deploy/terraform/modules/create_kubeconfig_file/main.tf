variable "filename" {
  type = string
  default = "kubeconfig"
}

data "external" "kubectl_config" {
  program = ["python", "${path.module}/file.py"]
}

resource "local_file" "kubeconfig" {
  content  = base64decode(data.external.kubectl_config.result.popa)
  filename = var.filename
}

output "file_path" {
  value = var.filename
}