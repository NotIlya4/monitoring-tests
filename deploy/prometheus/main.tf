variable "postgres_url" {
  type = string
}

variable "postgres_username" {
  type = string
}

variable "postgres_password" {
  type = string
}

resource "helm_release" "prometheus" {
  repository = "https://prometheus-community.github.io/helm-charts"
  chart = "kube-prometheus-stack"
  name  = "prometheus"
  
  namespace = "monitoring"
  create_namespace = true

  values = [file("values.yaml")]
  
  set {
    name  = "grafana.grafana\\.ini.database.host"
    value = var.postgres_url
  }

  set {
    name  = "grafana.grafana\\.ini.database.user"
    value = var.postgres_username
  }

  set {
    name  = "grafana.grafana\\.ini.database.password"
    value = var.postgres_password
  }
}