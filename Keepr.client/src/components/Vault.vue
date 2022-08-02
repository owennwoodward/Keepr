<template>


    <div class="row ">

        <div class="col-12 p-1  ">
            <div class="overview selectable" :style="{ 'background-image': `url(${vault.img})` }"
                @click="goToVaultPage">

                <h3 class="text-light ">{{ vault.name }}</h3>

            </div>
        </div>
    </div>

</template>


<script>
import { Modal } from 'bootstrap'
import { useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'

export default {
    props: {
        vault: {
            type: Object,
            required: true
        }
    },
    setup(props) {
        const router = useRouter()
        return {
            router,
            goToVaultPage() {
                router.push({
                    name: 'Vault',
                    params: {
                        vaultId: props.vault.id
                    }
                })
            },
        }
    }
}
</script>


<style lang="scss" scoped>
.overview {
    position: relative;
    height: 20vw;
    background-repeat: no-repeat;
    background-size: cover;
    // border-radius: 5%;

}

.over-top {
    z-index: 50;
    background-color: rgba(223, 200, 200, 0.347);
    backdrop-filter: blur(5px);
    color: white;
    text-shadow: 1px 1px #252222;
    // border-radius: 5%;

}

.profile-pic {

    width: 70px;
    height: 70px;
    border-radius: 50%;
    margin-left: 10px;
}
</style>