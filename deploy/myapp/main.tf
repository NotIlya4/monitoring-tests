variable "namespace" {
  default = "default"
}

locals {
  chart_path = "./${path.module}/chart/"
  chart_hash = sha1(join("", [for f in fileset(local.chart_path, "**/*.yaml"): filesha1("${local.chart_path}/${f}")]))
}

resource "helm_release" "myapp" {
  chart = local.chart_path
  name = "myapp"

  set {
    name  = "chart-hash"
    value = local.chart_hash
  }

  namespace = var.namespace
  create_namespace = true
}