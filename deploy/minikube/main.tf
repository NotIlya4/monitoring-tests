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
    create = "minikube delete && just minikube-start"
    update = "minikube start"
    delete = "minikube delete && just remove-tfstate && just remove-generated"
  }
}

resource "shell_script" "kubeconfig" {
  lifecycle_commands {
    create = "just kubeconfig"
    update = "just kubeconfig"
    delete = "just kubeconfig-delete"
  }
  
  depends_on = [shell_script.minikube]
}

output "kubeconfig_path" {
  value = "${abspath(path.root)}/kubeconfig.generated"
}

output "context" {
  value = "minikube"
}