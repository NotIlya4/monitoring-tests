include "root" {
  path = find_in_parent_folders()
}

include "kubernetes" {
  path = "../kubernetes-provider.hcl"
}

dependency "postgres" {
  config_path = "../postgres"
}

inputs = {
  postgres_url = "${dependency.postgres.outputs.host}:${dependency.postgres.outputs.port}"
  postgres_password = dependency.postgres.outputs.password
  postgres_username = dependency.postgres.outputs.username
}