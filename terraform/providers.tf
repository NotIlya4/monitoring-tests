provider "kubernetes" {
  alias = "kubernetes"
  config_path    = local.config_path
  config_context = local.config_context
}

provider "helm" {
  alias = "helm"
  kubernetes {
    config_path = local.config_path
    config_context = local.config_context
  }
}