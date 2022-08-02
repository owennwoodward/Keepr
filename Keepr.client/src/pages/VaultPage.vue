<template>
    <div class="container pt-5 mt-5">
        <div class="row ">
            <div class="col-10">
                <h1>{{ vault.name }}</h1>

            </div>
            <button class="col-2 btn btn-primary pt-2 mt-2 selectable" @click="deleteVault">
                <h5>Delete Vault</h5>
            </button>
        </div>
        <h4 class="pt-3">Keeps:</h4>
    </div>
</template>


<script>
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
import { router } from '../router'

export default {

    setup() {
        const route = useRoute()
        onMounted(async () => {
            try {
                await vaultsService.activeVault(route.params.vaultId)
            }
            catch (error) {
                logger.error(error);
                Pop.toast(error.message, "error")
            }
        })

        return {
            async deleteVault() {
                try {
                    if (await Pop.confirm()) {
                        await vaultsService.deleteVault(route.params.vaultId)
                        Pop.toast("Vault deleted", "success")
                        router.push({
                            name: 'Profile',
                            params: {
                                id:
                                    AppState.activeKeep.creator?.id
                            }
                        })
                    }
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            },
            vaults: computed(() => AppState.vaults),
            vault: computed(() => AppState.activeVault),
            keep: computed(() => AppState.keeps),
            account: computed(() => AppState.account),
            profile: computed(() => AppState.profile)
        }
    }
}
</script>


<style lang="scss" scoped>
</style>