terraform {
  required_providers {
    shell = {
      source = "scottwinkler/shell"
      version = "1.7.10"
    }
  }
}

provider "shell" {}

resource "shell_script" "kind" {
  lifecycle_commands {
    create = "just create"
    update = "just update"
    delete = "just delete"
  }
}

resource "shell_script" "kubeconfig" {
  lifecycle_commands {
    create = "just kubeconfig"
    update = "just kubeconfig"
    delete = "just kubeconfig-delete"
  }

  depends_on = [shell_script.kind]
}

output "kubeconfig_path" {
  value = "${abspath(path.root)}/kubeconfig.generated"
}

output "context" {
  value = "kind-monitoring-test"
}