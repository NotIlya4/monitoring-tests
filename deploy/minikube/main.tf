terraform {
  required_providers {
    shell = {
      source = "scottwinkler/shell"
      version = "1.7.10"
    }
  }
}

provider "shell" {}

resource "shell_script" "minikube" {
  lifecycle_commands {
    create = "task minikube"
    delete = "minikube delete"
  }
}

resource "null_resource" "kubeconfig" {
  provisioner "local-exec" {
    command = "kubectl config view > kubeconfig.generated"
  }
}

output "kubeconfig_path" {
  value = "${abspath(path.root)}/kubeconfig.generated"
}

output "context" {
  value = "minikube"
}