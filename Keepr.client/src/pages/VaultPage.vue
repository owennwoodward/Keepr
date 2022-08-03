<template>
    <div class="container pt-5 mt-5">
        <div class="row ">
            <div class="col-10">
                <!-- <h1>{{ vault.name }}</h1> -->

            </div>
            <button class="col-2 btn btn-primary pt-2 mt-2 selectable" v-if="vault.creatorId == account.id"
                @click="deleteVault">
                <h5>Delete Vault</h5>
            </button>
        </div>
        <h4 class="pt-3">Keeps: {{ keeps.length }}</h4>
        <div class="" v-for="k in keeps" :key="k.id">
            <Keep :keep="k" />
            <div class="row">
                <div class="col-12">
                </div>
                <div class="col-12" v-if="vault.creatorId == account.id">
                    <button class="btn btn-primary" @click="removeKeepFromVault(k.vaultKeepId, k.id)">Remove from
                        Vault</button>
                </div>
            </div>

        </div>
    </div>
</template>


<script>
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { router } from '../router'

export default {

    setup() {
        const route = useRoute()
        onMounted(async () => {
            try {
                await vaultsService.activeVault(route.params.vaultId)
                await vaultsService.getKeepFromVault(AppState.activeVault.id)
            }
            catch (error) {
                logger.error(error);
                Pop.toast(error.message, "error")
                router.push({ name: 'Home' })
            }
        })

        return {
            async deleteVault() {
                try {
                    if (await Pop.confirm("Do you want to delete this vault")) {
                        await vaultsService.deleteVault(AppState.activeVault.id)
                        Pop.toast("Vault deleted, sending you Home", "success")
                        router.push({
                            name: 'Home'
                        })
                    }
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            },

            async removeKeepFromVault(vaultKeepId, keepId) {
                if (await Pop.confirm("Do you want to remove this keep?")) {
                    await vaultKeepsService.removeKeep(vaultKeepId, keepId)
                    Pop.toast("Keep removed from vault", "success")
                }
            },

            account: computed(() => AppState.account),
            vault: computed(() => AppState.activeVault),
            keeps: computed(() => AppState.keeps),
            profile: computed(() => AppState.profile)
        }
    }
}
</script>


<style lang="scss" scoped>
</style>